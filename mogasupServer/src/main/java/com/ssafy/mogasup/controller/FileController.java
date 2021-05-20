package com.ssafy.mogasup.controller;

import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

import org.springframework.core.io.InputStreamResource;
import org.springframework.core.io.Resource;
import org.springframework.http.ContentDisposition;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

import com.ssafy.mogasup.model.BasicResponse;

import io.swagger.annotations.ApiOperation;

@CrossOrigin
@RestController
public class FileController {
	
	@GetMapping(value = "/download")
	@ApiOperation(value = "실행파일 다운로드")
	public Object DownloadFile() throws Exception{/*
		Path path = Paths.get("D:/MoGasup.zip");
//		Path path = Paths.get("k4a102.p.ssafy.io/home/ubuntu/backend/MoGasup.zip");
		String contentTpye = Files.probeContentType(path);
		
		HttpHeaders headers = new HttpHeaders();
		headers.add(HttpHeaders.CONTENT_TYPE, contentTpye);
		headers.setContentDisposition(ContentDisposition.builder("attachment")
		        .filename("MoGaSup.zip", StandardCharsets.UTF_8)
		        .build());
		
		Resource resource = new InputStreamResource(Files.newInputStream(path));
		return new ResponseEntity<>(resource,headers,HttpStatus.OK);*/
		BasicResponse result = new BasicResponse();
		result.message = "success";
		return result;
		
	}
	
	
}

