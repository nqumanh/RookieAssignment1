function createData(name, protein) {
    return { name, protein };
}

const rows = [
    createData('Frozen yoghurt', 4.0),
    createData('Ice cream sandwich', 4.3),
    createData('Eclair', 6.0),
    createData('Cupcake', 4.3),
    createData('Gingerbread', 3.9),
    createData('Meat', 14.3),
    createData('Sweet', 16.0),
    createData('Vegetables', 41.3),
];

export {
    rows
}