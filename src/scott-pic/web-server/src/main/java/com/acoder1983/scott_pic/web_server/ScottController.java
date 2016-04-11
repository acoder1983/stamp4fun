package com.acoder1983.scott_pic.web_server;

import java.util.concurrent.atomic.AtomicLong;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class ScottController {

    private final AtomicLong counter = new AtomicLong();

    @RequestMapping("/scott")
    public ScottResult searchScott(@RequestParam(value="searchKeys") String searchKeys) {
        // 
    }
}