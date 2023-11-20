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
            'model_type' => "post",
            'name' => 'Vi phạm chính sách cộng đồng',
            'description' => 'Vi phạm chính sách cộng đồng',
            'can_use' => true,
        ]);

        ReportType::create([
            'model_type' => "post",
            'name' => 'Vi phạm quy định về bài đăng',
            'description' => 'Vi phạm quy định về bài đăng',
            'can_use' => true,
        ]);

        ReportType::create([
            'model_type' => "post",
            'name' => 'Hình ảnh không phù hợp với bài viết',
            'description' => 'Hình ảnh không phù hợp với bài viết',
            'can_use' => true,
        ]);

        ReportType::create([
            'model_type' => "post",
            'name' => 'Lạc đề, thông tin dài dòng, khó hiểu',
            'description' => 'Lạc đề, thông tin khó hiểu',
            'can_use' => true,
        ]);

        ReportType::create([
            'model_type' => "comment",
            'name' => 'loại báo cáo 1 (bình luận)',
            'description' => 'loại báo cáo 1 (bình luận)',
            'can_use' => true,
        ]);

        ReportType::create([
            'model_type' => "comment",
            'name' => 'loại báo cáo 2 (bình luận)',
            'description' => 'loại báo cáo 2 (bình luận)',
            'can_use' => true,
        ]);

        ReportType::create([
            'model_type' => "topic",
            'name' => 'loại báo cáo 1 (bài thảo luận)',
            'description' => 'loại báo cáo 1 (bài thảo luận)',
            'can_use' => true,
        ]);

        ReportType::create([
            'model_type' => "topic",
            'name' => 'loại báo cáo 2 (bài thảo luận)',
            'description' => 'loại báo cáo 2 (bài thảo luận)',
            'can_use' => true,
        ]);

        ReportType::create([
            'model_type' => "topic_comment",
            'name' => 'loại báo cáo 1 (bình luận thảo luận)',
            'description' => 'loại báo cáo 1 (bình luận thảo luận)',
            'can_use' => true,
        ]);

        ReportType::create([
            'model_type' => "topic_comment",
            'name' => 'loại báo cáo 2 (bình luận thảo luận)',
            'description' => 'loại báo cáo 2 (bình luận thảo luận)',
            'can_use' => true,
        ]);
    }
}