

import java.io.File;
import java.io.FileFilter;
import java.io.IOException;

public class ProcNationPage {
	public static void main(String[] args) throws IOException {
		String nationsPath = args[0];
		File file = new File(nationsPath);
		File[] paths = file.listFiles();
		for (File p : paths) {
			// create nation.txt under p
			String nationDir = p.getAbsolutePath();
			String nationTxt = String.format("%s%s%s", nationDir, File.separator, "nation.txt");
			File nt = new File(nationTxt);
			if (!nt.exists()) {
				nt.createNewFile();
			}

			// find pdf files under p
			File[] pdfs = p.listFiles(new FileFilter() {

				@Override
				public boolean accept(File pathname) {
					return pathname.getName().endsWith(".pdf");
				}
			});

			for (File pdf : pdfs) {
				System.out.println(String.format("process %s", pdf.getAbsolutePath()));
				// extract page num
				String fileName = pdf.getName();
				fileName = fileName.substring(0, fileName.lastIndexOf("."));
				String pageNumStr = fileName.substring(fileName.lastIndexOf(" ") + 1);
				int pageNumLen = pageNumStr.length();
				for (int i = 0; i < 3 - pageNumLen; i++) {
					pageNumStr = "0" + pageNumStr;
				}

				// create page dir
				String pageDirStr = String.format("%s%s%s", p.getAbsolutePath(), File.separator, pageNumStr);
				File pageDir = new File(pageDirStr);
				pageDir.mkdirs();
				// copy pdf into it
				pdf.renameTo(new File(pageDir, String.format("%s.pdf", pageNumStr)));
				// del old pdf
				pdf.delete();
			}
		}
	}
}
