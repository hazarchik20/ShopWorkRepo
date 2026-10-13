import { Route, Routes } from "react-router-dom";
import Shop from "@pages/Shop/Shop";

const AppRouter = () => {
    return (
        <Routes>
            <Route path="/" element={<Shop />} />
            <Route path="login" element={<LoginMainPage/>}/>
        </Routes>
    );
};

export default AppRouter;