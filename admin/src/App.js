import React from 'react'
import Routes from './routes'
// import Dashboard from './pages/Dashboard'
// import ProductTable from './components/ProductManagement/ProductTable'
// import CustomerTable from './components/ViewCustomer'
// import CategoryManagement from './components/CategoryManagement'
// import Protected from './components/Protected'
// import { loginApi, logoutApi } from './apis/useApi'
// import { useNavigate } from 'react-router-dom'

function App() {
  // let navigate = useNavigate();

  // const [isLoggedIn, setisLoggedIn] = useState(null);
  // const login = (loginForm) => {
  //   loginApi(loginForm).then(function (response) {
  //     setisLoggedIn(true);
  //     navigate(`/dashboard/categories`);
  //   }).catch(function (response) {
  //     alert(response.response.data);
  //   });
  // };

  return (
    <Routes/>
  )
}

export default App
