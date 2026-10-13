import './styles/global.css'
import Header from "@components/layout/Header/Header";
import Router from "@components/common/Router/Router";

import { Toaster } from "react-hot-toast";

const App = () => {
    return (
        <div className="app">
            <Header />
            <main>
                <Router />
            </main>

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