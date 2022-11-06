import React, { useState } from 'react'
import { Routes, Route } from 'react-router-dom'
import Dashboard from './pages/Dashboard'
import Login from './pages/Login'
import ProductTable from './components/ProductManagement/ProductTable'
import CustomerTable from './components/ViewCustomer'
import CategoryManagement from './components/CategoryManagement'
import Protected from './components/Protected'
import { loginApi, logoutApi } from './apis/useApi'
import { useNavigate } from 'react-router-dom'

function App() {
  let navigate = useNavigate();

  const [isLoggedIn, setisLoggedIn] = useState(null);
  const login = (loginForm) => {
    loginApi(loginForm).then(function (response) {
      setisLoggedIn(true);
      navigate(`/dashboard/categories`);
    }).catch(function (response) {
      alert(response.response.data);
    });
  };
  const logout = () => {
    logoutApi().then((response) => {
      setisLoggedIn(false);
    })
  };

  return (
    <Routes>
      <Route path='/' element={<Login login={login} />} />
      <Route path='/dashboard' element={
        <Protected isLoggedIn={isLoggedIn}>
          <Dashboard logout={logout} />
        </Protected>} >
        <Route path="categories" element={<CategoryManagement />} />
        <Route path="products" element={<ProductTable />} />
        <Route path="customers" element={<CustomerTable />} />
      </Route>
    </Routes>
  )
}

export default App
