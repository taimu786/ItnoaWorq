// src/features/profile/profileService.ts

import api from "../../lib/axios";
import type {
  ProfileDto,
  SkillDto,
  EducationDto,
  ExperienceDto,
} from "../../types/profile";

export const getMyProfileApi = async (): Promise<ProfileDto> => {
  const res = await api.get<ProfileDto>("/Profiles/me");
  return res.data;
};

export const updateProfileApi = async (data: ProfileDto): Promise<void> => {
  await api.put("/Profiles/me", data);
};

export const updateSkillsApi = async (
  skills: SkillDto[]
): Promise<void> => {
  await api.put("/Profiles/me/skills", skills);
};

export const addEducationApi = async (
  dto: EducationDto
): Promise<void> => {
  await api.post("/Profiles/me/education", dto);
};

export const addExperienceApi = async (
  dto: ExperienceDto
): Promise<void> => {
  await api.post("/Profiles/me/experience", dto);
};