package com.acoder1983.scott_pic.util;

import junit.framework.TestCase;

public class TestStringUtils extends TestCase {
	@SuppressWarnings("static-access")
	public void testGetScottYear() {
		this.assertEquals("1840", StringUtils.getScottYear("1840"));
		this.assertEquals("1922", StringUtils.getScottYear("1922, Nov. 11"));
		this.assertEquals("2001", StringUtils.getScottYear("2001-06"));
		this.assertNull(StringUtils.getScottYear("1775"));
		this.assertNull(StringUtils.getScottYear("2175"));

		this.assertNull(StringUtils.getScottYear("19ab"));
		this.assertNull(StringUtils.getScottYear("xxx 1964 abc"));

		this.assertNull(StringUtils.getScottYear("1964 A21"));
		this.assertNull(StringUtils.getScottYear("1964 AB21"));
	}
}
