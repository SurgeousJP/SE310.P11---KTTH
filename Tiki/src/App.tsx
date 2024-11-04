import { ToastContainer } from "react-toastify";
import { AppProvider } from "@/contexts/app.context";
import useRouteElement from "./useRouteElement";
import ScrollToTop from "./utils/scrollToTop";

export default function App() {
  const routeElement = useRouteElement();

  return (
    <AppProvider>
      <ScrollToTop />
      <div className="overflow-x-hidden overflow-y-hidden">
        {routeElement}
        <ToastContainer position="top-right" />
      </div>
    </AppProvider>
  );
}
