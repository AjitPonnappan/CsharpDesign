
try
{
	PicoGK.Library.Go(0.5f, test.App.run);
}

catch (Exception e)
{
	// Apparently something went wrong, output here
	Console.WriteLine(e);
}
