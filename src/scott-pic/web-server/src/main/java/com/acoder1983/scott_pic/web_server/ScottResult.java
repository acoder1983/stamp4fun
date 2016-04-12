package com.acoder1983.scott_pic.web_server;

public class ScottResult {
        private String errMsg;
	private ArrayList<String> pages;

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
}
