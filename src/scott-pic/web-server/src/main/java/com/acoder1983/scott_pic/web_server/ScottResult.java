package com.acoder1983.scott_pic.web_server;

import java.util.ArrayList;

public class ScottResult {
	private String errMsg;
	private ArrayList<String> pages;
	private String prevSearch;
	private String nextSearch;

	public ScottResult() {
		errMsg = "";
		pages = new ArrayList<String>();
	}

	public String getErrMsg() {
		return errMsg;
	}

	public void setErrMsg(String msg) {
		errMsg = msg;
	}

	public ArrayList<String> getPages() {
		return pages;
	}

	public void setPages(ArrayList<String> pages) {
		this.pages = pages;
	}

	public String getPrevSearch() {
		return prevSearch;
	}

	public void setPrevSearch(String prevSearch) {
		this.prevSearch = prevSearch;
	}

	public String getNextSearch() {
		return nextSearch;
	}

	public void setNextSearch(String nextSearch) {
		this.nextSearch = nextSearch;
	}
}
