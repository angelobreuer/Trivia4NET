namespace Trivia4NET
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Program
    {
        private static void Main() => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            var service = new TriviaService();
            var myLovelyToken = await service.RequestTokenAsync();
            var enumerator = service.PaginateQuestions(myLovelyToken.SessionToken, pageSize: 20, categoryId: 31);

            while (await enumerator.MoveNext(CancellationToken.None))
            {
                var question = enumerator.Current;
                Console.WriteLine(question.Content);
            }

            Console.ReadLine();
        }
    }
}