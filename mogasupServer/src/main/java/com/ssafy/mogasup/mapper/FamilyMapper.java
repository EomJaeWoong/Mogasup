package com.ssafy.mogasup.mapper;

import java.util.List;

import com.ssafy.mogasup.dto.Family;

public interface FamilyMapper {
	public void createFamily(String name);
	public int findFamilyId();
	public void createUserFamily(int user_id, int family_id);
	public List<Family> familyList(int user_id);
}
