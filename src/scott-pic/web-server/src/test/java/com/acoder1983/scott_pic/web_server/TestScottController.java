package com.acoder1983.scott_pic.web_server;

import java.io.IOException;
import java.util.ArrayList;

import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.databind.JsonMappingException;

import junit.framework.TestCase;

public class TestScottController extends TestCase {
	@SuppressWarnings("static-access")
	public void testSplitSearchStr() {
		ScottController controller = new ScottController();
		String searchStr = "china 1992";
		String[] searchKeys = controller.splitSearchStr(searchStr);
		String[] expect = new String[] { "china", "1992" };
		this.assertEquals(expect.length, searchKeys.length);
		for (int i = 0; i < expect.length; i++) {
			this.assertEquals(expect[i], searchKeys[i]);
		}

		searchStr = "  look   2016 ";
		searchKeys = controller.splitSearchStr(searchStr);
		expect = new String[] { "look", "2016" };
		this.assertEquals(expect.length, searchKeys.length);
		for (int i = 0; i < expect.length; i++) {
			this.assertEquals(expect[i], searchKeys[i]);
		}
	}

	@SuppressWarnings("static-access")
	public void testParseNation() throws JsonParseException, JsonMappingException, IOException {
		ScottController controller = new ScottController();
		String nationFile = "testdata/nations.txt";
		String[] searchKeys = { "挪威" };
		this.assertEquals("norway", controller.parseNation(nationFile, searchKeys).nationEn);

		String[] searchKeys2 = { "中国" };
		this.assertNull(controller.parseNation(nationFile, searchKeys2));
	}

	@SuppressWarnings("static-access")
	public void testParsePageFile() {
		ScottController controller = new ScottController();
		String pageFile = "catalog/china/002/1.f.txt";
		this.assertEquals("catalog/china/", controller.parsePagePre(pageFile));
		this.assertEquals(2, controller.parsePageNum(pageFile));
		this.assertEquals(1, controller.parseSubPageNum(pageFile));
	}

	@SuppressWarnings("static-access")
	public void testPagesBetween() {
		ScottController controller = new ScottController();
		String page1 = "catalog/china/002/2.f.txt";
		String page2 = "catalog/china/003/4.f.txt";
		ArrayList<String> expect = new ArrayList<String>();
		expect.add("catalog/china/002/3.f.txt");
		expect.add("catalog/china/002/4.f.txt");
		expect.add("catalog/china/003/1.f.txt");
		expect.add("catalog/china/003/2.f.txt");
		expect.add("catalog/china/003/3.f.txt");

		this.assertEquals(expect, controller.pagesBetween(page1, page2, "/"));
	}

	@SuppressWarnings("static-access")
	public void testParsePages() {
		ArrayList<String> nationPages = new ArrayList<String>();
		nationPages.add("catalog/china/002/1.f.txt");
		nationPages.add("catalog/china/002/2.f.txt");
		nationPages.add("catalog/china/002/3.f.txt");
		nationPages.add("catalog/china/002/4.f.txt");

		nationPages.add("catalog/china/003/1.f.txt");
		nationPages.add("catalog/china/003/2.f.txt");
		nationPages.add("catalog/china/003/3.f.txt");
		nationPages.add("catalog/china/003/4.f.txt");

		ArrayList<String> yearPages = new ArrayList<String>();
		yearPages.add("catalog/china/002/1.f.txt");
		yearPages.add("catalog/china/002/4.f.txt");
		yearPages.add("catalog/china/003/2.f.txt");
		yearPages.add("catalog/china/003/3.f.txt");

		ArrayList<String> expect = new ArrayList<String>();
		expect.add("catalog/china/002/1.f.txt");
		expect.add("catalog/china/002/4.f.txt");
		expect.add("catalog/china/003/1.f.txt");
		expect.add("catalog/china/003/2.f.txt");
		expect.add("catalog/china/003/3.f.txt");

		ScottController controller = new ScottController();
		ArrayList<String> actual = controller.parsePages(nationPages, yearPages, "/");
		this.assertEquals(expect, actual);
	}
}
