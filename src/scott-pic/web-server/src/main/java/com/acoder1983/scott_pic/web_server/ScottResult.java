package com.acoder1983.scott_pic.web_server;

public class ScottResult {

    private final long id;
    private final String[] pages;

    public ScottResult(long id, String content) {
        this.id = id;
        this.content = content;
    }

    public long getId() {
        return id;
    }

    public String getContent() {
        return content;
    }
}