import { Routes, Route } from "react-router-dom";

import Shop from "@pages/Shop/Shop";
import Login from "@pages/Auth/Login/LoginMainPage";
import Register from "@pages/Auth/Register/RegisterMainPage";

const AppRouter = () => {
    return (
        <Routes>
            <Route path="/" element={<Shop />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
        </Routes>
    );
};

export default AppRouter;