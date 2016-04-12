package com.acoder1983.scott_pic.web_server;

import com.acoder1983.scott_pic.search_engine.TestSearchEngine;

import junit.framework.Test;
import junit.framework.TestCase;
import junit.framework.TestSuite;

/**
 * Unit test for simple App.
 */
public class AppTest extends TestCase {
	/**
	 * Create the test case
	 *
	 * @param testName
	 *            name of the test case
	 */
	public AppTest(String testName) {
		super(testName);
	}

	/**
	 * @return the suite of tests being tested
	 */
	public static Test suite() {
		TestSuite suite = new TestSuite(AppTest.class);
		suite.addTestSuite(TestScottController.class);
		suite.addTestSuite(TestSearchEngine.class);
		return suite;
	}

	/**
	 * Rigourous Test :-)
	 */
	public void testApp() {
		assertTrue(true);
	}
}
