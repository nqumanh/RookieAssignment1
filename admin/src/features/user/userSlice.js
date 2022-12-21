import { createSlice } from '@reduxjs/toolkit'
import { getCustomers } from 'services'

export const userSlice = createSlice({
    name: 'user',
    initialState: {
        users: []
    },
    reducers: {
        getUsers: (state, { payload }) => {
            state.users = payload
        },
    }
})

// Action creators are generated for each case reducer function
export const { getUsers } = userSlice.actions

export default userSlice.reducer

export function fetchUsers() {
    return async (dispatch) => {
        getCustomers().then((res) => {
            dispatch(getUsers(res.data))
        }).catch((err) => {
            console.log(err)
        })
    }
}