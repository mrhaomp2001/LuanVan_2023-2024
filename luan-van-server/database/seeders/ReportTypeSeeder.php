<?php

namespace Database\Seeders;

use App\Models\ReportType;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class ReportTypeSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //

        ReportType::create([
            'name' => 'Vi phạm chính sách cộng đồng',
            'description' => 'Vi phạm chính sách cộng đồng'
        ]);
        
        ReportType::create([
            'name' => 'Vi phạm quy định về bài đăng',
            'description' => 'Vi phạm quy định về bài đăng'
        ]);

        ReportType::create([
            'name' => 'Hình ảnh không phù hợp với bài viết',
            'description' => 'Hình ảnh không phù hợp với bài viết'
        ]);

        ReportType::create([
            'name' => 'Lạc đề, thông tin dài dòng, khó hiểu',
            'description' => 'Lạc đề, thông tin khó hiểu'
        ]);
    }
}
