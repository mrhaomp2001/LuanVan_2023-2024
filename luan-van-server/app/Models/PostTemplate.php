<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class PostTemplate extends Model
{
    use HasFactory;

    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'name',
        'content',
        "theme_color",
        "is_require_title",
        "is_require_image",
        "can_use",
    ];

    /**
     * The model's default values for attributes.
     *
     * @var array
     */
    protected $attributes = [
        "theme_color" => "#ffffff",
        'is_require_title' => false,
        'is_require_image' => false,
        'can_use' => false,
    ];

    /**
     * The attributes that should be cast.
     *
     * @var array<string, string>
     */
    protected $casts = [
        'is_require_title' => 'boolean', 
        'is_require_image' => 'boolean', 
        'can_use' => 'boolean', 
    ];

    public function posts()
    {
        return $this->hasMany(Post::class, "post_template_id");
    }
}