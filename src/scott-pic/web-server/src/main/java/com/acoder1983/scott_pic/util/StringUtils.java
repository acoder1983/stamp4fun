package com.acoder1983.scott_pic.util;

import java.util.ArrayList;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class StringUtils {
	public static String getScottYear(String string) {
		String year = null;
		String[] sList = splitString(string);
		if (sList.length > 0) {
			Pattern p = Pattern.compile("^(18|19|20)\\d{2}");
			Matcher m = p.matcher(sList[0]);
			if (m.find()) {
				year = m.group();
				if (sList.length > 1) {
					p = Pattern.compile("[A-Z]+\\d+");
					m = p.matcher(sList[1]);
					if (m.find()) {
						year = null;
					}
				}
			}
		}
		return year;
	}

	public static String[] splitString(String string) {
		String[] arr = string.split(" ");
		ArrayList<String> sList = new ArrayList<String>();
		for (String s : arr) {
			if (!s.trim().isEmpty()) {
				sList.add(s);
			}
		}
		return sList.toArray(new String[] {});
	}
}
