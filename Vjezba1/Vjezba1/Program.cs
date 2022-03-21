// ZADATAK 14

Task t1 = Task.Run(() =>
{
    Console.WriteLine($"Task 1 gubi vrijeme");
    Thread.Sleep(1000);
    Console.WriteLine($"Task 1 završio s gubljenjem vremena!");
});

Task t2 = Task.Run(() =>
{
    Console.WriteLine($"Task 2 gubi vrijeme");
    Thread.Sleep(1500);
    Console.WriteLine($"Task 2 završio s gubljenjem vremena!");
});

Task.WaitAll(t1, t2);  // ceka da svaki dani program završi
//Task.WaitAny(t1, t2);  -> prekida s cekanjem kada jedan od danih programa zavrsi 



// ZADATAk 15

static async Task DoSomeSleepingAsync()
{
    Console.WriteLine($"Sleeping started");
    await Task.Delay(2000);
    Console.WriteLine($"Sleeping completed");
}

static async Task SlumberParty()
{
    Task t1 = DoSomeSleepingAsync();
    Console.WriteLine($"Waiting on the sleeping to finish..");
    await Task.Delay(1000);
    t1.Wait();
}

await SlumberParty();