package com.acoder1983.scott_pic.search_engine;

import java.io.File;
import java.util.ArrayList;

import org.apache.commons.io.FileUtils;

import junit.framework.Test;
import junit.framework.TestCase;
import junit.framework.TestSuite;

public class TestSearchScott extends TestCase {
	/**
	 * Create the test case
	 *
	 * @param testName
	 *            name of the test case
	 */
	public TestSearchScott(String testName) {
		super(testName);
	}

	/**
	 * @return the suite of tests being tested
	 */
	public static Test suite() {
		return new TestSuite(TestSearchScott.class);
	}

	@SuppressWarnings("static-access")
	public void testSearchScott() throws Exception {

		String scottCatalogPath = "testdata/build1st";
		String indexPath = String.format("%s%s%s", scottCatalogPath, File.separator, "index");

		FileUtils.deleteDirectory(new File(indexPath));

		Builder.build(scottCatalogPath, indexPath);

		Searcher searcher = new Searcher(indexPath);
		ArrayList<String> results = searcher.search("content", "1942");
		this.assertEquals(3, results.size());
	}
}
