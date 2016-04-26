import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.nio.file.FileVisitResult;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.SimpleFileVisitor;
import java.nio.file.attribute.BasicFileAttributes;
import java.security.NoSuchAlgorithmException;

import javax.imageio.ImageIO;

public class RmoveBlackLine {

	public static void main(String[] args) throws NoSuchAlgorithmException, IOException {
		findDocs(Paths.get(args[0]));
	}

	static void findDocs(Path path) throws IOException, NoSuchAlgorithmException {
		if (Files.isDirectory(path)) {
			Files.walkFileTree(path, new SimpleFileVisitor<Path>() {
				@Override
				public FileVisitResult visitFile(Path file, BasicFileAttributes attrs) throws IOException {
					try {
						procDoc(file);
					} catch (NoSuchAlgorithmException e) {
						e.printStackTrace();
					}
					return FileVisitResult.CONTINUE;
				}
			});
		} else {
			procDoc(path);
		}
	}

	static void procDoc(Path p) throws FileNotFoundException, NoSuchAlgorithmException, IOException {
		if (p.toString().endsWith(".jpg")) {
			removeBlackLines(p.toFile());
		}
	}

	static void removeBlackLines(File f) {
		try {
			BufferedImage bufImg = ImageIO.read(f);
			int height = bufImg.getHeight();
			int width = bufImg.getWidth();
			if (height == 1507 && width == 275) {
				int color = bufImg.getRGB(274, 0) & 0xFFFFFF;
				if (color == 0) {
					color = bufImg.getRGB(273, 0);
					for (int i = 0; i < 1507; ++i)
						bufImg.setRGB(274, i, color);
					ImageIO.write((BufferedImage) bufImg, "jpg", f);
					System.out.println(f.toString());
				}
			}

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}
