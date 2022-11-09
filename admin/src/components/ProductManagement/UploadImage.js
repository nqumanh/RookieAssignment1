import React, { useState } from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import { Stack } from '@mui/system';
import CloudUploadIcon from '@mui/icons-material/CloudUpload';

export default function UploadImage() {
    // const [file, setFile] = useState("");
    const [imagePreviewUrl, setImagePreviewUrl] = useState(null);
    const handleImageChange = (e) => {
        e.preventDefault();

        let reader = new FileReader();
        let file = e.target.files[0];

        reader.onloadend = () => {
            // setFile(file);
            setImagePreviewUrl(reader.result)
        }

        reader.readAsDataURL(file)

        const url = window.URL.createObjectURL(file)
        let urlSplit = url.split("/")
        const link = document.createElement("a");
        link.href = url;
        link.setAttribute("download", urlSplit[urlSplit.length - 1] + ".png"); //or any other extension
        document.body.appendChild(link);
        link.click();
        link.parentNode.removeChild(link);  
    }

    return (
        <div>
            <Stack direction="row" sx={{ display: "flex", justifyContent: "center" }}>
                <Button sx={{ alignContent: "middle", color: "#000", width: "200px" }} component="label">
                    <input hidden accept="image/*" multiple type="file" onChange={handleImageChange} />
                    <Box component="span" sx={{ p: 5, border: '1px dashed grey', textAlign: "center" }}>
                        <CloudUploadIcon /> <br></br>
                        Choose the image here
                    </Box>
                </Button>
            </Stack>
            <Stack direction="row" sx={{ display: "flex", justifyContent: "center" }}>
                <img src={imagePreviewUrl} alt="" style={{ width: "300px" }} />
            </Stack>
        </div >
    );
}
