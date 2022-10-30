import React from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Dashboard from './pages/Dashboard'
import Login from './pages/SignIn'
import ProductTable from './components/ProductManagement/ProductTable'
import CustomerTable from './components/CustomerTable'
import OrderTable from './components/OrderTable'
import CategoryManagement from './components/CategoryManagement'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Login />} />
        <Route path='/dashboard' element={<Dashboard />} />
        <Route path='/dashboard' element={<Dashboard />} >
          <Route path="categories" element={<CategoryManagement />} />
          <Route path="products" element={<ProductTable />} />
          <Route path="customers" element={<CustomerTable />} />
          <Route path="orders" element={<OrderTable />} />
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App
