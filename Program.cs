
try
{
	PicoGK.Library.Go(0.4f,HeatExchanger.Demo.Run);
}

catch (Exception e)
{
	// Apparently something went wrong, output here
	Console.WriteLine(e);
}
