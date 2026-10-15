
import { Outlet } from "react-router-dom";

import SearchBar from "@components/common/SearchBar/SearchBar";
import ProfileNavigation from "@components/profile/ProfileNavigation/ProfileNavigation";

import "./ProfileLayout.scss";

const ProfileLayout = () => {
    return (
        <div className="profile-page">
            <div className="profile-search">
                <div className="profile-search-inner">
                    <SearchBar variant="default" />
                </div>
            </div>

            <div className="profile-container">
                <ProfileNavigation />

                <main className="profile-content">
                    <Outlet />
                </main>
            </div>
        </div>
    );
};

export default ProfileLayout;
