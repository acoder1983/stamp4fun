package com.acoder1983.scott_pic.web_server;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

import com.acoder1983.scott_pic.search_engine.Builder;

@SpringBootApplication
public class Application {

	public static void main(String[] args) {
		if (args[0].equals("-build") && args.length != 4) {
			String catalogPath = args[1];
			String indexPath = args[2];
			Builder.build(catalogPath, indexPath);
		} else if (args[0].equals("-run") && args.length > 2) {
			ScottController.INDEX_PATH = args[1];
			ScottController.NATION_FILE = args[2];
			int others = 3;
			String[] otherArgs = new String[] {};
			if (args.length > others) {
				otherArgs = new String[args.length - others];
				for (int i = 0; i < otherArgs.length; ++i) {
					otherArgs[i] = args[i + others];
				}
			}
			SpringApplication.run(Application.class, otherArgs);
		} else {
			printUsageMsg();
		}
	}

	private static void printUsageMsg() {
		System.out.println(
				"input: -build [catalogPath] [indexPath] or -run [indexPath] [nationFile] --spring.config.location=");
	}

}
