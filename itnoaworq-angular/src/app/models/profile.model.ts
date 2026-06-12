export interface SkillDto {
  id: string;
  name: string;
  proficiency: number;
}

export interface EducationDto {
  id: string;
  school: string;
  degree: string;
  startDate: string;
  endDate?: string | null;
  description?: string | null;
}

export interface ExperienceDto {
  id: string;
  title: string;
  companyName: string;
  startDate: string;
  endDate?: string | null;
  description?: string | null;
  isCurrent: boolean;
}

export interface ProfileDto {
  id: string;
  userId: string;
  headline: string;
  summary: string;
  profession: string;
  location: string;
  skills: SkillDto[];
  education: EducationDto[];
  experience: ExperienceDto[];
}