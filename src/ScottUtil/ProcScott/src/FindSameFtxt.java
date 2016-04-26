
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.math.BigInteger;
import java.nio.MappedByteBuffer;
import java.nio.channels.FileChannel;
import java.nio.file.FileVisitResult;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.SimpleFileVisitor;
import java.nio.file.attribute.BasicFileAttributes;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;

public class FindSameFtxt {

	static String DUMMY_TXT_MD5_1 = "4cc18d009e8eb9a894d9fd07897aba0c";
	static String DUMMY_TXT_MD5_2 = "a77b5206524dd75b7fe89a3e726c4661";

	public static void main(String[] args) throws IOException, NoSuchAlgorithmException {
		indexDocs(Paths.get(args[0]));
		for (int i = 0; i < md5s.size(); ++i) {
			for (int j = i + 1; j < md5s.size(); ++j) {
				if (!md5s.get(i).toLowerCase().equals(DUMMY_TXT_MD5_1)
						&& !md5s.get(i).toLowerCase().equals(DUMMY_TXT_MD5_2)
						&& md5s.get(i).toLowerCase().equals(md5s.get(j).toLowerCase())) {
					System.out.println(files.get(i));
					System.out.println(files.get(j));
					System.out.println("");
					break;
				}
			}
		}
	}

	static ArrayList<String> md5s = new ArrayList<String>();
	static ArrayList<String> files = new ArrayList<String>();

	static void indexDocs(Path path) throws IOException, NoSuchAlgorithmException {
		if (Files.isDirectory(path)) {
			Files.walkFileTree(path, new SimpleFileVisitor<Path>() {
				@Override
				public FileVisitResult visitFile(Path file, BasicFileAttributes attrs) throws IOException {
					try {
						indexDoc(file);
					} catch (NoSuchAlgorithmException e) {
						e.printStackTrace();
					}
					return FileVisitResult.CONTINUE;
				}
			});
		} else {
			indexDoc(path);
		}
	}

	static void indexDoc(Path file) throws FileNotFoundException, NoSuchAlgorithmException, IOException {
		if (file.toString().endsWith(".f.txt")) {
			md5s.add(getMd5ByFile(file.toFile()));
			files.add(file.toString());
		}
	}

	public static String getMd5ByFile(File file) throws FileNotFoundException, IOException, NoSuchAlgorithmException {
		String value = null;
		try (FileInputStream in = new FileInputStream(file);) {

			MappedByteBuffer byteBuffer = in.getChannel().map(FileChannel.MapMode.READ_ONLY, 0, file.length());
			MessageDigest md5 = MessageDigest.getInstance("MD5");
			md5.update(byteBuffer);
			BigInteger bi = new BigInteger(1, md5.digest());
			value = bi.toString(16);
		}
		return value;
	}
}
