<?php

namespace Database\Seeders;

use App\Models\PostTemplate;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class PostTemplateSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        PostTemplate::create(
            [
                'name' => "Mẫu số 1",
                'content' => "Nội dung quy định mẫu số 1",
            ]
        );
    }
}
