package com.acoder1983.scott_pic.util;

public class DateUtils {
	public static boolean isScottYear(String string) {
		try {
			return string.length() > 3 && Integer.valueOf(string.substring(0, 4)) > 1830
					&& Integer.valueOf(string.substring(0, 4)) < 2020;
		} catch (Exception e) {
			return false;
		}
	}
}
