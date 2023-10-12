<?php

namespace Database\Seeders;

use App\Models\Role;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class RoleSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        Role::create([
            'name' => "user",
            'description' => "người dùng",
        ]);

        Role::create([
            'name' => "moderator",
            'description' => "quản trị nội dung",
        ]);

        Role::create([
            'name' => "admin",
            'description' => "quản trị hệ thống",
        ]);
    }
}
