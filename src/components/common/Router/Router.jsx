import { Routes, Route } from "react-router-dom";

import Shop from "@pages/Shop/Shop";
import Login from "@pages/Auth/Login/LoginMainPage";
import Register from "@pages/Auth/Register/RegisterMainPage";
import Profile from "@pages/Account/Profile";

const AppRouter = () => {
    return (
        <Routes>
            <Route path="/" element={<Shop />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/profile" element={<Profile />} />
            
        </Routes>
    );
};

export default AppRouter;