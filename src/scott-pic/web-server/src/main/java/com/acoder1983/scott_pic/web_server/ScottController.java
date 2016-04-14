package com.acoder1983.scott_pic.web_server;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Map;

import org.apache.commons.io.FileUtils;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.acoder1983.scott_pic.search_engine.Searcher;
import com.acoder1983.scott_pic.util.DateUtils;
import com.acoder1983.scott_pic.util.Logger;
import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.ObjectMapper;

@RestController
public class ScottController {

	public static String INDEX_PATH;

	public static String NATION_FILE;

	@CrossOrigin(maxAge = 3600)
	@RequestMapping("/scott")
	public ScottResult searchScott(@RequestParam(value = "search") String searchStr) throws Exception {
		Logger.info("search string: " + searchStr);
		ScottResult result = new ScottResult();

		String[] searchKeys = splitSearchStr(searchStr);

		String nation = parseNation(NATION_FILE, searchKeys);
		Logger.info("nation: " + nation);
		String year = parseYear(searchKeys);
		Logger.info("year: " + year);

		if (nation == null || year == null) {
			result.setErrMsg("input nation+year");
		} else {
			Searcher searcher = new Searcher(INDEX_PATH);
			ArrayList<String> nationPages = searcher.search("path", nation);
			Logger.info("nationPages num: " + nationPages.size());

			ArrayList<String> yearPages = searcher.search("years", year);
			Logger.info("yearPages num: " + yearPages.size());

			ArrayList<String> pages = parsePages(nationPages, yearPages, File.separator);
			for (int i = 0; i < pages.size(); ++i) {
				pages.set(i, pages.get(i).replace("f.txt", "jpg"));
			}
			Logger.info("pages num: " + yearPages.size());

			result.setPages(pages);
		}
		return result;
	}

	public ArrayList<String> parsePages(ArrayList<String> nationPages, ArrayList<String> yearPages, String fileSep) {
		nationPages.sort(null);
		yearPages.sort(null);

		ArrayList<String> nyPages = new ArrayList<String>();
		for (int i = 0; i < nationPages.size(); i++) {
			for (int j = 0; j < yearPages.size(); j++) {
				if (nationPages.get(i).equals(yearPages.get(j))) {
					if (!nyPages.isEmpty()) {
						String page1 = nyPages.get(nyPages.size() - 1);
						String page2 = nationPages.get(i);
						ArrayList<String> pages = pagesBetween(page1, page2, fileSep);
						if (pages.size() == 1) {
							nyPages.add(pages.get(0));
						}
					}
					nyPages.add(nationPages.get(i));
					break;
				}
			}
		}

		return nyPages;
	}

	public ArrayList<String> pagesBetween(String page1, String page2, String fileSep) {
		ArrayList<String> pages = new ArrayList<String>();
		String pathPre = parsePagePre(page1);
		int p1 = parsePageNum(page1);
		int ps1 = parseSubPageNum(page1);
		int p2 = parsePageNum(page2);
		int ps2 = parseSubPageNum(page2);
		int num = (p2 - p1) * 4 + ps2 - ps1 - 1;
		for (int i = 0; i < num; i++) {
			if (ps1 < 4) {
				ps1 += 1;
			} else {
				p1 += 1;
				ps1 = 1;
			}
			String pStr = String.valueOf(p1);
			int len = pStr.length();
			for (int j = 0; j < 3 - len; j++) {
				pStr = "0" + pStr;
			}
			pages.add(String.format("%s%s%s%s.f.txt", pathPre, pStr, fileSep, ps1));
		}
		return pages;
	}

	public int parseSubPageNum(String pageFile) {
		return Integer.valueOf(pageFile.substring(pageFile.length() - 7, pageFile.length() - 6));
	}

	public int parsePageNum(String pageFile) {
		return Integer.valueOf(pageFile.substring(pageFile.length() - 11, pageFile.length() - 8));
	}

	public String parsePagePre(String pageFile) {
		return pageFile.substring(0, pageFile.length() - 11);
	}

	private String parseYear(String[] searchKeys) {
		for (String key : searchKeys) {
			if (DateUtils.isScottYear(key)) {
				return key;
			}
		}
		return null;
	}

	String parseNation(String nationFile, String[] searchKeys)
			throws JsonParseException, JsonMappingException, IOException {
		ObjectMapper MAPPER = new ObjectMapper();
		String content = FileUtils.readFileToString(new File(nationFile), "UTF-8");
		Map<String, String> nationMap = MAPPER.readValue(content, new TypeReference<Map<String, String>>() {
		});
		for (String key : searchKeys) {
			if (nationMap.containsKey(key)) {
				return nationMap.get(key);
			}
		}
		return null;
	}

	public String[] splitSearchStr(String searchStr) {
		String[] arr = searchStr.split(" ");
		ArrayList<String> sList = new ArrayList<String>();
		for (String s : arr) {
			if (!s.trim().isEmpty()) {
				sList.add(s);
			}
		}
		return sList.toArray(new String[] {});
	}
}
