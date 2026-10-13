import Header from "@components/layout/Header/Header";
import Router from "@components/common/Router/Router";
import './styles/index.scss'

import { useLocation } from "react-router-dom";
import { Toaster } from "react-hot-toast";

const App = () => {
    const { pathname } = useLocation();

    const isAuthPage = ["/login", "/register"].includes(pathname);
    return (
         <div className="app">
            {!isAuthPage && <Header />}

            <main>
                <Router />
            </main>

            {/* {!isAuthPage && <Footer />} */}

            <Toaster 
                position="top-center"
                toastOptions={{
                    duration: 2000,
                    success: {
                        icon: null,
                        style: {
                            background: '#16a34a',
                            color: '#fff',
                        },
                    },
                    error: {
                        icon: null,
                        style: {
                            background: '#dc2626',
                            color: '#fff',
                        },
                    },
                }}
            />
        </div>
    );
}

export default App;