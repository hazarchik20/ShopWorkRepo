import Header from "@components/layout/Header/Header";
import Footer from "@components/layout/Footer/Footer";
import Router from "@components/common/Router/Router";
import './styles/index.scss'

import { useLocation } from "react-router-dom";

const App = () => {
    const { pathname } = useLocation();

    const isNeedHeader = ["/", "/add","/card"].includes(pathname);
    const isNeedFooter = ["login", "/register"].includes(pathname);

    return (
         <div className="app">
            {isNeedHeader && <Header />}
              <main>
                  <Router />
              </main>
            {!isNeedFooter && <Footer />}
        </div>
    );
}

export default App;