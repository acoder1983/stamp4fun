package procScott;

public class Main {

	public static void main(String[] args) {
		if (args.length != 2) {
			System.out.println("usage: java -jar ProcScott.jar [-pnp or -pny] [path]");
			return;
		}

		String nationsPath = args[1];
		try {
			switch (args[0]) {
			case "-pnp":
				ProcNationPage.procPages(nationsPath);
				break;
			default:
				throw new Exception("no " + args[0]);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

}
