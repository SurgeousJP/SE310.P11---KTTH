import LoginModals from "./components/Modals/LoginModals";
import { RegisterModals } from "./components/Modals/RegisterModals/RegisterModals";
import AddProduct from "./pages/Ecommerce/AddProduct";
import Ecommerce from "./pages/Ecommerce/Ecommerce";
import EcommerceAdmin from "./pages/Ecommerce/EcommerceAdmin";
import { EcommerceProductDetails } from "./pages/Ecommerce/EcommerceProductDetail";
import { Navigate, useNavigate, useRoutes } from "react-router-dom";
import EditProduct from "./pages/Ecommerce/EditProduct";

export default function useRouteElement() {
  const navigate = useNavigate();
  const routeElement = useRoutes([
    {
      element: <AddProduct />,
      path: '/add-product'
    },
    {
      element: <EditProduct />,
      path: '/edit-product/:id'
    },
    {
      element: <Ecommerce />,
      path: '/landing-page'
    },
    {
      element: <EcommerceProductDetails />,
      path: '/product-detail'
    },
    {
      element: <EcommerceAdmin />,
      path: '/admin-list'
    },
    {
      element: (
        <LoginModals
          openModal={true}
          onCloseModal={function (): void {
            navigate("/");
          }}
          onSignUpClick={function (): void {
            navigate("register");
          }}
          onForgotPassClick={function (): void {
            navigate("forgot-password");
          }}
        />
      ),
      path: "login",
    },
    {
      element: (
        <RegisterModals
          openModal={true}
          onCloseModal={function (): void {
            navigate("/");
          }}
          onSignInClick={function (): void {
            navigate("login");
          }}
        />
      ),
      path: "register",
    },
    {
      path: '*', // This matches all paths
      element: <Navigate to="/login" replace />, // Redirect to login as the default route
    },
  ]);
  return routeElement;
}
