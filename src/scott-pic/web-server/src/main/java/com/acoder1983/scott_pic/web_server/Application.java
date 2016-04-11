package com.acoder1983.scott_pic.web_server;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class Application {

    public static void main(String[] args) {
        if (args.Length() != 1) {
            System.out.println("input: scott_nation_path.xml")
        }
        String scottNationFile=args[0];
        search_engine.Builder.load(scottNationFile);
        SpringApplication.run(Application.class, args);
    }
}
