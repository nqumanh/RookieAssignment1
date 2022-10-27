import React from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Dashboard from './pages/Dashboard'
import Login from './pages/SignIn'
import CategoryTable from './components/CategoryTable'
import ProductTable from './components/ProductTable'
import CustomerTable from './components/CustomerTable'
import OrderTable from './components/OrderTable'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Login />} />
        <Route path='/dashboard' element={<Dashboard />} />
        <Route path='/dashboard' element={<Dashboard />} >
          <Route path="categories" element={<CategoryTable />} />
          <Route path="products" element={<ProductTable />} />
          <Route path="customers" element={<CustomerTable />} />
          <Route path="orders" element={<OrderTable />} />
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App
