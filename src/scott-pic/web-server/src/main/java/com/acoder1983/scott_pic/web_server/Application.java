package com.acoder1983.scott_pic.web_server;

import java.io.File;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

import com.acoder1983.scott_pic.search_engine.Builder;

@SpringBootApplication
public class Application {

	public static void main(String[] args) {
		if (args.length != 2) {
			printUsageMsg();
		}
		if (args[0].equals("-build")) {
			String scottCatalogPath = args[0];
			Builder.build(scottCatalogPath, String.format("%s%s%s", scottCatalogPath, File.separator, "index"));
		} else if (args[0].equals("-run")) {
			SpringApplication.run(Application.class, args);
		} else {
			printUsageMsg();
		}
	}

	private static void printUsageMsg() {
		System.out.println("input: -load or -run scott_nation_path.xml");
	}

}
