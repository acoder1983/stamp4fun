package com.acoder1983.scott_pic.search_engine;

import java.io.IOException;
import java.nio.file.Paths;
import java.util.ArrayList;

import org.apache.lucene.analysis.Analyzer;
import org.apache.lucene.analysis.standard.StandardAnalyzer;
import org.apache.lucene.document.Document;
import org.apache.lucene.index.DirectoryReader;
import org.apache.lucene.index.IndexReader;
import org.apache.lucene.queryparser.classic.QueryParser;
import org.apache.lucene.search.IndexSearcher;
import org.apache.lucene.search.Query;
import org.apache.lucene.search.ScoreDoc;
import org.apache.lucene.search.TopDocs;
import org.apache.lucene.store.FSDirectory;

/** Simple command-line based search demo. */
public class Searcher {

	public Searcher(String indexPath) throws IOException {
		reader = DirectoryReader.open(FSDirectory.open(Paths.get(indexPath)));
		searcher = new IndexSearcher(reader);
		analyzer = new StandardAnalyzer();
	}

	Analyzer analyzer;
	IndexSearcher searcher;
	IndexReader reader;

	public ArrayList<String> search(String field, String content) throws Exception {
		if (field == null || content == null) {
			throw new Exception("must input [field] and [content]");
		}

		QueryParser parser = new QueryParser(field, analyzer);
		Query query = parser.parse(content);

		TopDocs results = searcher.search(query, 1000000000);
		ScoreDoc[] hits = results.scoreDocs;
		ArrayList<String> pages = new ArrayList<String>();
		for (int i = 0; i < results.totalHits; ++i) {
			Document doc = searcher.doc(hits[i].doc);
			pages.add(doc.get("path"));
		}
		return pages;
	}

}
