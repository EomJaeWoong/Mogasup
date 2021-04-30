package com.ssafy.mogasup;

import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;

@MapperScan(basePackages = {"com.ssafy.mogasup.mapper"})
@EnableConfigurationProperties
@SpringBootApplication
public class MogasupServerApplication {

	public static void main(String[] args) {
		SpringApplication.run(MogasupServerApplication.class, args);
	}

}
