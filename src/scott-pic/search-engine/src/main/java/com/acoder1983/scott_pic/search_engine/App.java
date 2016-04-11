package com.acoder1983.scott_pic.search_engine;

/**
 * Hello world!
 *
 */
public class App {
	public static void main(String[] args) {
        if (args.Length() != 1) {
            System.out.println("input: scott_nation_path.xml")
        }
        String scottNationFile=args[0];
        search_engine.Builder.build(scottNationFile);
	}

}
