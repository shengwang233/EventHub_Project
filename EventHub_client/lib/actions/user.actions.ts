"use serve";

import { cookies } from "next/headers";

export const getCurrentUser = async () => {
  try {
    const res = await fetch("http://localhost:5000/api/account/me", {
      credentials: "include",
      headers: {
        Cookie: cookies().toString(),
      },
      cache: "no-store",
    });

    if (!res.ok) return null;

    const data = await res.json();
    console.log("🟢 getCurrentUser data:", data);
    return data;
  } catch (error) {
    console.error("Failed to fetch user", error);
    return null;
  }
};
