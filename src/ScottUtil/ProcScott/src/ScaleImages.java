import java.awt.Color;
import java.awt.Graphics2D;
import java.awt.Image;
import java.awt.geom.AffineTransform;
import java.awt.image.AffineTransformOp;
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

public class ScaleImages {

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
						System.out.println(file.toString() + " " + e.getMessage());
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
			resize(p.toFile(), 275, 1507);
		}
	}

	static void resize(File f, int width, int height) {
		try {
			double ratio = 0; // ���ű���
			BufferedImage bi = ImageIO.read(f);
			Image itemp = bi.getScaledInstance(width, height, BufferedImage.SCALE_SMOOTH);
			// �������
			if ((bi.getHeight() - height > 50) || (bi.getWidth() > width)) {
				if (bi.getHeight() > bi.getWidth()) {
					ratio = (new Integer(height)).doubleValue() / bi.getHeight();
				} else {
					ratio = (new Integer(width)).doubleValue() / bi.getWidth();
				}
				AffineTransformOp op = new AffineTransformOp(AffineTransform.getScaleInstance(ratio, ratio), null);
				itemp = op.filter(bi, null);
				BufferedImage image = new BufferedImage(width, height, BufferedImage.TYPE_INT_RGB);
				Graphics2D g = image.createGraphics();
				g.setColor(Color.white);
				g.fillRect(0, 0, width, height);
				if (width == itemp.getWidth(null))
					g.drawImage(itemp, 0, (height - itemp.getHeight(null)) / 2, itemp.getWidth(null),
							itemp.getHeight(null), Color.white, null);
				else
					g.drawImage(itemp, (width - itemp.getWidth(null)) / 2, 0, itemp.getWidth(null),
							itemp.getHeight(null), Color.white, null);
				g.dispose();
				removeBlackLines(f, image);
				itemp = image;
				ImageIO.write((BufferedImage) itemp, "jpg", f);
				System.out.println("scale " + f.toString());
			}
		} catch (IOException e) {
			System.out.println(f.toString() + " " + e.getMessage());
		}
	}

	static void removeBlackLines(File f, BufferedImage bufImg) {
		int height = bufImg.getHeight();
		int width = bufImg.getWidth();
		if (height == 1507 && width == 275) {
			for (int j = 270; j < 275; ++j) {
				int color = bufImg.getRGB(j, 0) & 0xFFFFFF;
				if (color == 0) {
					color = bufImg.getRGB(0, 0);
					for (int i = 0; i < 1507; ++i)
						bufImg.setRGB(j, i, color);
				}

			}
		}
	}

}
