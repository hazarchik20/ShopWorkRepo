import { Routes, Route } from "react-router-dom";

import Shop from "@pages/Shop/Shop";
import Login from "@pages/Auth/Login/LoginMainPage";
import Register from "@pages/Auth/Register/RegisterMainPage";

import ProfileLayout from "@pages/Profile/ProfileLayout";
import ProfileOverview from "@pages/Profile/Overview/ProfileOverview";
import ProfileOrders from "@pages/Profile/Orders/ProfileOrders";
import ProfileListings from "@pages/Profile/Listings/ProfileListings";


const AppRouter = () => {
    return (
        <Routes>
            <Route path="/" element={<Shop />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/profile" element={<ProfileLayout />}>
                <Route index element={<ProfileOverview />} />
                <Route path="orders" element={<ProfileOrders />} />
                <Route path="listings" element={<ProfileListings />} />
            </Route>

        </Routes>
    );
};

export default AppRouter;