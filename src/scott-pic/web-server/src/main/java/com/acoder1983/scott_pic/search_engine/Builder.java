package com.acoder1983.scott_pic.search_engine;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.nio.file.FileVisitResult;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.SimpleFileVisitor;
import java.nio.file.attribute.BasicFileAttributes;
import java.util.Date;

import org.apache.lucene.analysis.Analyzer;
import org.apache.lucene.analysis.standard.StandardAnalyzer;
import org.apache.lucene.document.Document;
import org.apache.lucene.document.Field;
import org.apache.lucene.document.Field.Store;
import org.apache.lucene.document.LongPoint;
import org.apache.lucene.document.TextField;
import org.apache.lucene.index.IndexWriter;
import org.apache.lucene.index.IndexWriterConfig;
import org.apache.lucene.index.IndexWriterConfig.OpenMode;
import org.apache.lucene.store.Directory;
import org.apache.lucene.store.FSDirectory;

import com.acoder1983.scott_pic.util.DateUtils;

public class Builder {
	public static void build(String docsPath, String indexPath) {
		if (docsPath == null || indexPath == null) {
			System.err.println("must input docsPath and indexPath");
			System.exit(1);
		}

		final Path docDir = Paths.get(docsPath);
		if (!Files.isReadable(docDir)) {
			System.out.println("Document directory '" + docDir.toAbsolutePath()
					+ "' does not exist or is not readable, please check the path");
			System.exit(1);
		}

		Date start = new Date();
		try {
			System.out.println("Indexing to directory '" + indexPath + "'...");

			Directory dir = FSDirectory.open(Paths.get(indexPath));
			Analyzer analyzer = new StandardAnalyzer();
			IndexWriterConfig iwc = new IndexWriterConfig(analyzer);

			// Add new documents to an existing index:
			iwc.setOpenMode(OpenMode.CREATE_OR_APPEND);

			// Optional: for better indexing performance, if you
			// are indexing many documents, increase the RAM
			// buffer. But if you do this, increase the max heap
			// size to the JVM (eg add -Xmx512m or -Xmx1g):
			//
			// iwc.setRAMBufferSizeMB(256.0);

			IndexWriter writer = new IndexWriter(dir, iwc);
			indexDocs(writer, docDir);

			writer.close();

			Date end = new Date();
			System.out.println(end.getTime() - start.getTime() + " total milliseconds");

		} catch (IOException e) {
			System.out.println(" caught a " + e.getClass() + "\n with message: " + e.getMessage());
		}
	}

	static void indexDocs(final IndexWriter writer, Path path) throws IOException {
		if (Files.isDirectory(path)) {
			Files.walkFileTree(path, new SimpleFileVisitor<Path>() {
				@Override
				public FileVisitResult visitFile(Path file, BasicFileAttributes attrs) throws IOException {
					indexDoc(writer, file, attrs.lastModifiedTime().toMillis());
					return FileVisitResult.CONTINUE;
				}
			});
		} else {
			indexDoc(writer, path, Files.getLastModifiedTime(path).toMillis());
		}
	}

	/** Indexes a single document */
	static void indexDoc(IndexWriter writer, Path file, long lastModified) throws IOException {
		if (file.toString().endsWith(".f.txt")) {
			// make a new, empty document
			Document doc = new Document();

			Field pathField = new TextField("path", file.toString(), Field.Store.YES);
			doc.add(pathField);

			doc.add(new LongPoint("modified", lastModified));

			String yearLines = "";
			String allLines = "";
			try (BufferedReader reader = new BufferedReader(new FileReader(file.toString()))) {
				String tempString = null;
				while ((tempString = reader.readLine()) != null) {
					if (tempString.length() > 3 && DateUtils.isScottYear(tempString.substring(0, 4))) {
						yearLines += " " + tempString;
					}
					allLines += " " + tempString;
				}
			}
			doc.add(new TextField("contents", allLines, Store.NO));
			doc.add(new TextField("years", yearLines, Store.NO));

			System.out.println("adding " + file);
			// writer.updateDocument(new Term("path", file.toString()), doc);
			writer.addDocument(doc);
		}
	}

}
