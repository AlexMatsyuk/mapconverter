using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit; 
using TextCopy;

namespace Booking;

public static class BookingPage
{
    public static void ShowReviews(string url)
    {
        IBrowser browser = null;
        try
        {
            Task<IPlaywright> playwrightTask = Playwright.CreateAsync();
            Task.WaitAll(playwrightTask);
            IPlaywright playwright = playwrightTask.Result;

            Task<IBrowser> browserTask = playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            Task.WaitAll(browserTask);
            browser = browserTask.Result;

            Task<IBrowserContext> contextTask = browser.NewContextAsync();
            Task.WaitAll(contextTask);
            IBrowserContext context = contextTask.Result;

            Task<IPage> pageTask = context.NewPageAsync();
            Task.WaitAll(pageTask);
            IPage page = pageTask.Result;

            // Открываем страницу
            Task task = page.GotoAsync(url);
            Task.WaitAll(task);

            // открываем отзывы
            Task task2 = page.Locator("(//a[@rel='reviews'])[1]").Nth(0).ClickAsync();
            Task.WaitAll(task2);

            // дожидаемся что отзывы загрузились
            var dialogLocator = page.Locator("//div[@role='dialog'][.//button]");
            Task task3 = dialogLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            Task.WaitAll(task3);
            
            // ставим сортировку по дате
            Task task31 = page.SelectOptionAsync("#reviewListSorters", "NEWEST_FIRST");
            Task.WaitAll(task31);
            // Task task31 = page.Locator("//select[@name='reviewListSorters']").Nth(0).ClickAsync();
            // Task.WaitAll(task31);
            // Thread.Sleep(1000);
            // var optionLocator = page.Locator("//option[@value='NEWEST_FIRST']");
            // Task task32 = optionLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            // Task.WaitAll(task32);
            // Task task33 = optionLocator.Nth(0).ClickAsync();
            // Task.WaitAll(task33);
            
            Thread.Sleep(1000);
                
            List<Review> reviews = new();
            
            for(;;)
            {
                List<ILocator> reviewLocators = page.Locator("//div[@data-testid='review-card']").AllAsync().Result.ToList();
                reviews.AddRange(reviewLocators.ConvertAll(GetReview));
                
                var nextButtonLocator = page.Locator("//button[@aria-label='Следующая страница'][not(@disabled)]");
                if (nextButtonLocator.CountAsync().Result > 0)
                {
                    var currentPage = page.Locator("//button[@aria-current='page']");
                    string initialText = currentPage.InnerTextAsync().Result;
                    
                    Task task4 = nextButtonLocator.ClickAsync();
                    Task.WaitAll(task4);
                    Thread.Sleep(500);
                    Assertions.Expect(currentPage).ToHaveTextAsync(new Regex($"^(?!{initialText}).*$"));
                    Thread.Sleep(500);
                }
                else
                {
                    break;
                }
            }
            string allReviews = $"{reviews.Count} отзывов\n\n" + string.Join("\n\n", reviews.ConvertAll(review => review.GetInfo()));
            ClipboardService.SetText(allReviews);
            Console.WriteLine(allReviews);
        }
        finally
        {
            Task task4 = browser?.CloseAsync();
            if (task4 != null)
            {
                Task.WaitAll(task4);
            }
        }
    }
    
    public static Review GetReview(ILocator locator)
    {
        Review review = new Review();

        string rating = locator.Locator("//div[@data-testid='review-score']/div/div[2]").InnerTextAsync().Result;
        review.Rating = int.Parse(rating.Trim().Replace(",0",string.Empty).Replace(".0", string.Empty));
        
        string date = locator.Locator("//span[@data-testid='review-date']").InnerTextAsync().Result;
        review.Date = Regex.Replace(date, "^.*?: ", string.Empty);
        
        string country = locator.Locator("//div[@data-testid='review-avatar']/div/div[2]/div[2]").InnerTextAsync().Result.Trim();
        review.Country = Regex.Replace(country, "^.*?\n", string.Empty);
        
        review.Room = locator.Locator("//span[@data-testid='review-room-name']").InnerTextAsync().Result;
        
        review.Title = locator.Locator("//h4").InnerTextAsync().Result.Trim();

        var prosLocator = locator.Locator("//div[@data-testid='review-positive-text']"); 
        review.Pros = prosLocator.CountAsync().Result > 0 ? prosLocator.InnerTextAsync().Result.Trim() : string.Empty;
        
        var consLocator = locator.Locator("//div[@data-testid='review-negative-text']");
        review.Cons = consLocator.CountAsync().Result > 0 ? consLocator.InnerTextAsync().Result.Trim() : string.Empty;

        return review;
    }
}