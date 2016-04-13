
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
import java.util.HashSet;
import java.util.Set;

public class FindSameFtxt {

	public static void main(String[] args) throws IOException, NoSuchAlgorithmException {
		indexDocs(Paths.get(args[0]));
		Set<String> paths = new HashSet<String>();
		for (int i = 0; i < md5s.size(); ++i) {
			for (int j = i + 1; j < md5s.size(); ++j) {
				if (md5s.get(i).equals(md5s.get(j))) {
					String p = files.get(i).substring(0, files.get(i).length() - 11);
					if (!paths.contains(p)) {
						paths.add(p);
						System.out.println(p);
						break;
					}
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
						// TODO Auto-generated catch block
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
