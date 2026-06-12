import { useEffect } from "react";
import MainLayout from "../../components/Layouts/MainLayout";
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import { fetchProfile } from "../../features/profile/profileSlice";
import ProfileHeader from "../../components/profile/ProfileHeader";
import AboutSection from "../../components/profile/AboutSection";
import ExperienceSection from "../../components/profile/ExperienceSection";
import EducationSection from "../../components/profile/EducationSection";

export default function ProfilePage() {
  const dispatch = useAppDispatch();
  const { profile, loading } = useAppSelector((s) => s.profile);

  useEffect(() => {
    dispatch(fetchProfile());
  }, [dispatch]);

  if (loading || !profile) return <div>Loading...</div>;

  return (
    <MainLayout layout="profile">
      <ProfileHeader profile={profile} />
      <AboutSection profile={profile} />
      <ExperienceSection experience={profile.experience} />
      <EducationSection education={profile.education} />
    </MainLayout>
  );
}