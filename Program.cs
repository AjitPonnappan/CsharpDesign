
try
{
	//First steps to creating impeller geometry
	PicoGK.Library.Go(0.5f,test.App.geometry);
}

catch (Exception e)
{
	// Apparently something went wrong, output here
	Console.WriteLine(e);
}
